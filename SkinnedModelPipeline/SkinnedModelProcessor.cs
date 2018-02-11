#region File Description
//-----------------------------------------------------------------------------
// SkinnedModelProcessor.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using SkinnedModel;
#endregion

namespace SkinnedModelPipeline
{
    /// <summary>
    /// Custom processor extends the builtin framework ModelProcessor class,
    /// adding animation support.
    /// </summary>
    [ContentProcessor]
    public class SkinnedModelProcessor : ModelProcessor
    {
        /// <summary>
        /// The main Process method converts an intermediate format content pipeline
        /// NodeContent tree to a ModelContent object with embedded animation data.
        /// </summary>
        public override ModelContent Process(NodeContent input,
                                             ContentProcessorContext context)
        {
            ValidateMesh(input, context, null);

            BoneContent skeleton = MeshHelper.FindSkeleton(input);
            if (skeleton == null)
                throw new InvalidContentException("Input skeleton not found.");

            // We don't want to have to worry about different parts of the model being
            // in different local coordinate systems, so let's just bake everything.
            FlattenTransforms(input, skeleton);

            // Read the bind pose and skeleton hierarchy data.
            IList<BoneContent> bones = MeshHelper.FlattenSkeleton(skeleton);

            if (bones.Count > SkinnedEffect.MaxBones)
            {
                throw new InvalidContentException(string.Format(
                    "Skeleton has {0} bones, but the maximum supported is {1}.",
                    bones.Count, SkinnedEffect.MaxBones));
            }

            List<Matrix> bindPose = new List<Matrix>();
            List<Matrix> inverseBindPose = new List<Matrix>();
            List<int> skeletonHierarchyP = new List<int>();
            List<int[]> skeletonHierarchyC = new List<int[]>();

            foreach (BoneContent bone in bones)
            {
                bindPose.Add(bone.Transform);
                inverseBindPose.Add(Matrix.Invert(bone.AbsoluteTransform));
                skeletonHierarchyP.Add(bones.IndexOf(bone.Parent as BoneContent));
                List<int> cid=new List<int>();
                foreach (BoneContent bc in bone.Children)
                    cid.Add(bones.IndexOf(bc));
                skeletonHierarchyC.Add(cid.ToArray());
            }

            Dictionary<string, int> boneMap = BuildUpBoneNameIndexTable(bones);
            // Convert animation data to our runtime format.
            Dictionary<string, AnimationClip> animationClips;
            if (spliterFile.Length == 0)
                animationClips = ProcessAnimations(skeleton.Animations, boneMap);
            else
                animationClips = ProcessSplitAnimations(skeleton.Animations, boneMap);

            // Chain to the base ModelProcessor class so it can convert the model data.
            ModelContent model = base.Process(input, context);

            // Store our custom animation data in the Tag property of the model.
            model.Tag = new SkinningData(animationClips,
                bindPose,
                inverseBindPose,
                skeletonHierarchyP,
                skeletonHierarchyC,
                boneMap);

            return model;
        }

        /// <summy>
        /// Build up a table mapping bone names to indices.
        /// </summy>
        Dictionary<string, int> BuildUpBoneNameIndexTable(IList<BoneContent> bones)
        {
            Dictionary<string, int> boneMap = new Dictionary<string, int>();
            for (int i = 0; i < bones.Count; i++)
            {
                string boneName = bones[i].Name;

                if (!string.IsNullOrEmpty(boneName))
                    boneMap.Add(boneName, i);
            }
            return boneMap;
        }

        Dictionary<string, AnimationClip> ProcessSplitAnimations(
            AnimationContentDictionary animations,
            Dictionary<string, int> boneMap)
        {
            IEnumerator<KeyValuePair<string, AnimationContent>> aniIter = animations.GetEnumerator();
            aniIter.MoveNext();
            Dictionary<string, AnimationClip> processed =
                SplitAndCreateAnimation(ProcessAnimationKeyframe(aniIter.Current.Value, boneMap));

            if (processed.Count == 0)
                throw new InvalidContentException("Input file does not contain any animations.");

            return processed;
        }

        /// <summary>
        /// Converts an intermediate format content pipeline AnimationContentDictionary
        /// object to our runtime AnimationClip format.
        /// </summary>
        Dictionary<string, AnimationClip> ProcessAnimations(
            AnimationContentDictionary animations,
            Dictionary<string, int> boneMap)
        {
            // Convert each animation in turn.
            Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();

            foreach (KeyValuePair<string, AnimationContent> animation in animations)
                animationClips.Add(animation.Key, ProcessAnimation(animation.Value, boneMap));

            if (animationClips.Count == 0)
                throw new InvalidContentException("Input file does not contain any animations.");

            return animationClips;
        }
        
        /// <summary>
        /// Converts an intermediate format content pipeline AnimationContent
        /// object to our runtime AnimationClip format AND Split By SplitFile
        /// </summary>
        Dictionary<string, AnimationClip> SplitAndCreateAnimation(List<Keyframe> keyframes)
        {
            AnimationSpliter spliter = new AnimationSpliter(spliterFile);
            List<KeyValuePair<string, int>> splitData = spliter.SplitData;
            List<Keyframe>[] splitedKeyframes = new List<Keyframe>[splitData.Count];

            int multiplyOffset = (int)(keyframes.Count / spliter.KeyframeCount);

            int index = 0;
            int begin = splitData[0].Value;
            while (index < splitData.Count - 1)
            {
                int end = splitData[index + 1].Value;
                splitedKeyframes[index++] = keyframes.GetRange(begin * multiplyOffset, (end - begin) * multiplyOffset);
                begin = end;
            }
            splitedKeyframes[index] = keyframes.GetRange(begin * multiplyOffset, keyframes.Count - begin * multiplyOffset);

            Dictionary<string, AnimationClip> aniClip = new Dictionary<string, AnimationClip>();
            for (int c = 0; c < splitData.Count; ++c)
                aniClip.Add(splitData[c].Key, RescaledKeyframe(splitedKeyframes[c]));
            return aniClip;
        }

        //, int multiplyOffset
        // int skipCount,
        static AnimationClip RescaledKeyframe(List<Keyframe> currKeyframes)
        {
            List<Keyframe> rescaledKeyframes;
            TimeSpan offTime = currKeyframes[0].Time;
            rescaledKeyframes = new List<Keyframe>(currKeyframes.Count);
            foreach (Keyframe kf in currKeyframes)
                rescaledKeyframes.Add(new Keyframe(kf.Bone, kf.Time - offTime, kf.Transform));
            return new AnimationClip(rescaledKeyframes[rescaledKeyframes.Count - 1].Time, rescaledKeyframes);
        }

        /// <summary>
        /// Converts an intermediate format content pipeline AnimationContent
        /// object to our runtime AnimationClip format.
        /// </summary>
        static AnimationClip ProcessAnimation(AnimationContent animation,
                                              Dictionary<string, int> boneMap)
        {
            return new AnimationClip(animation.Duration, ProcessAnimationKeyframe(animation, boneMap));
        }

        static List<Keyframe> ProcessAnimationKeyframe(AnimationContent animation, Dictionary<string, int> boneMap)
        {
            List<Keyframe> keyframes = new List<Keyframe>();

            foreach (KeyValuePair<string, AnimationChannel> channel in animation.Channels)
            {
                // Look up what bone this channel is controlling.
                int boneIndex;

                if (!boneMap.TryGetValue(channel.Key, out boneIndex))
                    throw new InvalidContentException(string.Format(
                        "Found animation for bone '{0}', " +
                        "which is not part of the skeleton.",
                        channel.Key));

                // Convert the keyframe data.
                foreach (AnimationKeyframe keyframe in channel.Value)
                    keyframes.Add(new Keyframe(boneIndex, keyframe.Time, keyframe.Transform));
            }

            keyframes.Sort((Keyframe a, Keyframe b) => { return a.Time.CompareTo(b.Time); });

            if (keyframes.Count == 0)
                throw new InvalidContentException("Animation has no keyframes.");

            if (animation.Duration <= TimeSpan.Zero)
                throw new InvalidContentException("Animation has a zero duration.");

            return keyframes;
        }

        /// <summary>
        /// Makes sure this mesh contains the kind of data we know how to animate.
        /// </summary>
        static void ValidateMesh(NodeContent node, ContentProcessorContext context,
                                 string parentBoneName)
        {
            MeshContent mesh = node as MeshContent;

            if (mesh != null)
            {
                // Validate the mesh.
                if (parentBoneName != null)
                {
                    context.Logger.LogWarning(null, null,
                        "Mesh {0} is a child of bone {1}. SkinnedModelProcessor " +
                        "does not correctly handle meshes that are children of bones.",
                        mesh.Name, parentBoneName);
                }

                if (!MeshHasSkinning(mesh))
                {
                    context.Logger.LogWarning(null, null,
                        "Mesh {0} has no skinning information, so it has been deleted.",
                        mesh.Name);

                    mesh.Parent.Children.Remove(mesh);
                    return;
                }
            }
            else if (node is BoneContent)
            {
                // If this is a bone, remember that we are now looking inside it.
                parentBoneName = node.Name;
            }

            // Recurse (iterating over a copy of the child collection,
            // because validating children may delete some of them).
            foreach (NodeContent child in new List<NodeContent>(node.Children))
                ValidateMesh(child, context, parentBoneName);
        }

        /// <summary>
        /// Checks whether a mesh contains skininng information.
        /// </summary>
        static bool MeshHasSkinning(MeshContent mesh)
        {
            foreach (GeometryContent geometry in mesh.Geometry)
                if (!geometry.Vertices.Channels.Contains(VertexChannelNames.Weights()))
                    return false;
            return true;
        }

        /// <summary>
        /// Bakes unwanted transforms into the model geometry,
        /// so everything ends up in the same coordinate system.
        /// </summary>
        static void FlattenTransforms(NodeContent node, BoneContent skeleton)
        {
            foreach (NodeContent child in node.Children)
            {
                // Don't process the skeleton, because that is special.
                if (child == skeleton)
                    continue;

                // Bake the local transform into the actual geometry.
                MeshHelper.TransformScene(child, child.Transform);

                // Having baked it, we can now set the local
                // coordinate system back to identity.
                child.Transform = Matrix.Identity;

                // Recurse.
                FlattenTransforms(child, skeleton);
            }
        }

        /// <summary>
        /// Force all the materials to use our skinned model effect.
        /// </summary>
        [DefaultValue(MaterialProcessorDefaultEffect.SkinnedEffect)]
        public override MaterialProcessorDefaultEffect DefaultEffect
        {
            get { return MaterialProcessorDefaultEffect.SkinnedEffect; }
            set { }
        }

        private string spliterFile = "";
        /// <summary>
        /// Animation Spliter File
        /// </summary>
        [DefaultValue("")]
        public string SpliterFile
        {
            get { return spliterFile; }
            set { spliterFile = value; }
        }
    }
}
