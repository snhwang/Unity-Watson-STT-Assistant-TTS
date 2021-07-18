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

namespace IBM.Watson.CompareComply.V1.Model
{
    /// <summary>
    /// A pair of `nature` and `party` objects. The `nature` object identifies the effect of the element on the
    /// identified `party`, and the `party` object identifies the affected party.
    /// </summary>
    public class Label
    {
        /// <summary>
        /// The identified `nature` of the element.
        /// </summary>
        [JsonProperty("nature", NullValueHandling = NullValueHandling.Ignore)]
        public string Nature { get; set; }
        /// <summary>
        /// The identified `party` of the element.
        /// </summary>
        [JsonProperty("party", NullValueHandling = NullValueHandling.Ignore)]
        public string Party { get; set; }
    }
}
