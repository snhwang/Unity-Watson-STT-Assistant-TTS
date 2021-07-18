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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.ToneAnalyzer.V3.Model
{
    /// <summary>
    /// The tone analysis results for the input from the general-purpose endpoint.
    /// </summary>
    public class ToneAnalysis
    {
        /// <summary>
        /// The results of the analysis for the full input content.
        /// </summary>
        [JsonProperty("document_tone", NullValueHandling = NullValueHandling.Ignore)]
        public DocumentAnalysis DocumentTone { get; set; }
        /// <summary>
        /// An array of `SentenceAnalysis` objects that provides the results of the analysis for the individual
        /// sentences of the input content. The service returns results only for the first 100 sentences of the input.
        /// The field is omitted if the `sentences` parameter of the request is set to `false`.
        /// </summary>
        [JsonProperty("sentences_tone", NullValueHandling = NullValueHandling.Ignore)]
        public List<SentenceAnalysis> SentencesTone { get; set; }
    }
}
