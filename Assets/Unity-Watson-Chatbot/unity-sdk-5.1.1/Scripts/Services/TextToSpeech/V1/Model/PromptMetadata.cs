/**
* (C) Copyright IBM Corp. 2021.
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

namespace IBM.Watson.TextToSpeech.V1.Model
{
    /// <summary>
    /// Information about the prompt that is to be added to a custom model. The following example of a `PromptMetadata`
    /// object includes both the required prompt text and an optional speaker model ID:
    ///
    /// `{ "prompt_text": "Thank you and good-bye!", "speaker_id": "823068b2-ed4e-11ea-b6e0-7b6456aa95cc" }`.
    /// </summary>
    public class PromptMetadata
    {
        /// <summary>
        /// The required written text of the spoken prompt. The length of a prompt's text is limited to a few sentences.
        /// Speaking one or two sentences of text is the recommended limit. A prompt cannot contain more than 1000
        /// characters of text. Escape any XML control characters (double quotes, single quotes, ampersands, angle
        /// brackets, and slashes) that appear in the text of the prompt.
        /// </summary>
        [JsonProperty("prompt_text", NullValueHandling = NullValueHandling.Ignore)]
        public string PromptText { get; set; }
        /// <summary>
        /// The optional speaker ID (GUID) of a previously defined speaker model that is to be associated with the
        /// prompt.
        /// </summary>
        [JsonProperty("speaker_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SpeakerId { get; set; }
    }
}
