#region File Description
//-----------------------------------------------------------------------------
// AnimationPlayer.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace SkinnedModel
{
    /// <summary>
    /// The animation player is in charge of decoding bone position
    /// matrices from an animation clip.
    /// </summary>
    public class AnimationPlayer
    {
        // Information about the currently playing animation clip.
        AnimationClip currentClipValue;
        TimeSpan currentTimeValue;
        int currentKeyframe;

        // Current animation transform matrices.
        Matrix[] boneTransforms;
        Matrix[] worldTransforms;
        Matrix[] skinTransforms;

        // Backlink to the bind pose and skeleton hierarchy data.
        SkinningData skinningDataValue;

        private bool loopAnimation = true;
        public bool LoopAnimation
        {
            get { return loopAnimation; }
            set { loopAnimation = value; }
        }

        public event EventHandler AnimationFinished;

        /// <summary>
        /// Constructs a new animation player.
        /// </summary>
        public AnimationPlayer(SkinningData skinningData)
        {
            if (skinningData == null)
                throw new ArgumentNullException("skinningData");

            skinningDataValue = skinningData;

            boneTransforms = new Matrix[skinningData.BindPose.Count];
            worldTransforms = new Matrix[skinningData.BindPose.Count];
            skinTransforms = new Matrix[skinningData.BindPose.Count];
        }


        /// <summary>
        /// Starts decoding the specified animation clip.
        /// </summary>
        public void StartClip(AnimationClip clip)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");

            currentClipValue = clip;
            currentTimeValue = TimeSpan.Zero;
            currentKeyframe = 0;

            // Initialize bone transforms to the bind pose.
            skinningDataValue.BindPose.CopyTo(boneTransforms, 0);
        }

        /// <summary>
        /// Advances the current animation position.
        /// </summary>
        public void Update(TimeSpan time, Matrix rootTransform)
        {
            UpdateBoneTransforms(time);
            if (EnableExtraTransforms)
                UpdateWorldTransformsEx(rootTransform);
            else
                UpdateWorldTransforms(rootTransform);
            UpdateSkinTransforms();
        }

        /// <summary>
        /// Helper used by the Update method to refresh the BoneTransforms data.
        /// </summary>
        private void UpdateBoneTransforms(TimeSpan time)
        {
            if (currentClipValue == null)
                throw new InvalidOperationException("AnimationPlayer.Update was called before StartClip");

            // Update the animation time.
            if (loopAnimation)
            {
                time += currentTimeValue;

                // If we reached the end, loop back to the start.
                if (time >= currentClipValue.Duration)
                {
                    AnimationFinished(this, null);
                    do
                        time -= currentClipValue.Duration;
                    while (time >= currentClipValue.Duration);
                }
            }

            if ((time < TimeSpan.Zero) || (time >= currentClipValue.Duration))
                throw new ArgumentOutOfRangeException("time");

            // If the position moved backwards, reset the keyframe index.
            if (time < currentTimeValue)
            {
                currentKeyframe = 0;
                skinningDataValue.BindPose.CopyTo(boneTransforms, 0);
            }

            currentTimeValue = time;

            // Read keyframe matrices.
            List<Keyframe> keyframes = currentClipValue.Keyframes;
            while (currentKeyframe < keyframes.Count)
            {
                Keyframe keyframe = keyframes[currentKeyframe];
                // Stop when we've read up to the current time position.
                if (keyframe.Time > currentTimeValue)
                    break;
                // Use this keyframe.
                boneTransforms[keyframe.Bone] = keyframe.Transform;
                currentKeyframe++;
            }
        }


        /// <summary>
        /// Helper used by the Update method to refresh the WorldTransforms data.
        /// </summary>
        private void UpdateWorldTransforms(Matrix rootTransform)
        {
            // Root bone.
            worldTransforms[0] = boneTransforms[0] * rootTransform;

            // Child bones.
            for (int bone = 1; bone < worldTransforms.Length; bone++)
            {
                int parentBone = skinningDataValue.ParentSkeletonHierarchy[bone];
                worldTransforms[bone] = boneTransforms[bone] * worldTransforms[parentBone];
            }
        }

        public void AddExtraTransforms(int index, Matrix matrix)
        {
            EnableExtraTransforms = true;
            ExTransIndex.Add(index);
            ExTransMatrix.Add(matrix);
        }
        public void ClearExtraTransforms()
        {
            ExTransIndex.Clear();
            ExTransMatrix.Clear();
            ExTransLerpAmount.Clear();
            EnableExtraTransforms = false;
        }
        private bool EnableExtraTransforms = false;
        private List<int> ExTransIndex = new List<int>();
        private List<float> ExTransLerpAmount = new List<float>();
        private List<Matrix> ExTransMatrix = new List<Matrix>();
        private void UpdateWorldTransformsEx(Matrix rootTransform)
        {
            // Root bone.
            worldTransforms[0] = boneTransforms[0] * rootTransform;

            // Child bones.
            for (int bone = 1; bone < worldTransforms.Length; bone++)
            {
                int parentBone = skinningDataValue.ParentSkeletonHierarchy[bone];
                int index = ExTransIndex.IndexOf(bone);
                worldTransforms[bone] = boneTransforms[bone] * worldTransforms[parentBone];
                if (index >= 0)
                {
                    Vector3 trans = worldTransforms[parentBone].Translation;
                    Matrix InvTransMat = Matrix.CreateTranslation(-trans);
                    Matrix TransMat = Matrix.CreateTranslation(trans);
                    worldTransforms[bone] = worldTransforms[bone] * InvTransMat * ExTransMatrix[index] * TransMat;
                }
            }
        }

        /// <summary>
        /// Helper used by the Update method to refresh the SkinTransforms data.
        /// </summary>
        private void UpdateSkinTransforms()
        {
            for (int bone = 0; bone < skinTransforms.Length; bone++)
                skinTransforms[bone] = skinningDataValue.InverseBindPose[bone] * worldTransforms[bone];
        }

        /// <summary>
        /// Gets the current bone transform matrices, relative to their parent bones.
        /// </summary>
        public Matrix[] GetBoneTransforms()
        {
            return boneTransforms;
        }


        /// <summary>
        /// Gets the current bone transform matrices, in absolute format.
        /// </summary>
        public Matrix[] GetWorldTransforms()
        {
            return worldTransforms;
        }


        /// <summary>
        /// Gets the current bone transform matrices,
        /// relative to the skinning bind pose.
        /// </summary>
        public Matrix[] GetSkinTransforms()
        {
            return skinTransforms;
        }


        /// <summary>
        /// Gets the clip currently being decoded.
        /// </summary>
        public AnimationClip CurrentClip
        {
            get { return currentClipValue; }
        }


        /// <summary>
        /// Gets the current play position.
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return currentTimeValue; }
        }
    }
}
