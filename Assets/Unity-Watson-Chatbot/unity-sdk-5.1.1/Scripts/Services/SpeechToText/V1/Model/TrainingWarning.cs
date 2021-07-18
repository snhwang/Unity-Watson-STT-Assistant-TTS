/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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

using Newtonsoft.Json;

namespace IBM.Watson.SpeechToText.V1.Model
{
    /// <summary>
    /// A warning from training of a custom language or custom acoustic model.
    /// </summary>
    public class TrainingWarning
    {
        /// <summary>
        /// An identifier for the type of invalid resources listed in the `description` field.
        /// </summary>
        public class CodeValue
        {
            /// <summary>
            /// Constant INVALID_AUDIO_FILES for invalid_audio_files
            /// </summary>
            public const string INVALID_AUDIO_FILES = "invalid_audio_files";
            /// <summary>
            /// Constant INVALID_CORPUS_FILES for invalid_corpus_files
            /// </summary>
            public const string INVALID_CORPUS_FILES = "invalid_corpus_files";
            /// <summary>
            /// Constant INVALID_GRAMMAR_FILES for invalid_grammar_files
            /// </summary>
            public const string INVALID_GRAMMAR_FILES = "invalid_grammar_files";
            /// <summary>
            /// Constant INVALID_WORDS for invalid_words
            /// </summary>
            public const string INVALID_WORDS = "invalid_words";
            
        }

        /// <summary>
        /// An identifier for the type of invalid resources listed in the `description` field.
        /// Constants for possible values can be found using TrainingWarning.CodeValue
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
        /// <summary>
        /// A warning message that lists the invalid resources that are excluded from the custom model's training. The
        /// message has the following format: `Analysis of the following {resource_type} has not completed successfully:
        /// [{resource_names}]. They will be excluded from custom {model_type} model training.`.
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
