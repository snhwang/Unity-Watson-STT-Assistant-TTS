﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Widgetss;
using UnityEngine;

namespace IBM.Cloud.SDK.DataTypes
{
    /// <summary>
    /// This class holds a AudioClip and maximum sample level.
    /// </summary>
    public class AudioData : Widget.Data
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioData()
        { }

        /// <exclude />
        ~AudioData()
        {
            UnityObjectUtil.DestroyUnityObject(Clip);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="clip">The AudioClip.</param>
        /// <param name="maxLevel">The maximum sample level in the audio clip.</param>
        public AudioData(AudioClip clip, float maxLevel)
        {
            Clip = clip;
            MaxLevel = maxLevel;
        }
        /// <summary>
        /// Name of this data type.
        /// </summary>
        /// <returns>The readable name.</returns>
        public override string GetName()
        {
            return "Audio";
        }

        /// <summary>
        /// The AudioClip.
        /// </summary>
        public AudioClip Clip { get; set; }
        /// <summary>
        /// The maximum level in the audio clip.
        /// </summary>
        public float MaxLevel { get; set; }
    }
}
