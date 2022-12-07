﻿// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace Project_ART
{
    public partial class MLCandidateRanking
    {
        /// <summary>
        /// model input class for MLCandidateRanking.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(0)]
            [ColumnName(@"Candidate_Details_ID")]
            public float Candidate_Details_ID { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"Introduction_Video_Data")]
            public string Introduction_Video_Data { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"DISC_Personality")]
            public string DISC_Personality { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLCandidateRanking.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Candidate_Details_ID")]
            public float Candidate_Details_ID { get; set; }

            [ColumnName(@"Introduction_Video_Data")]
            public float[] Introduction_Video_Data { get; set; }

            [ColumnName(@"DISC_Personality")]
            public uint DISC_Personality { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("MLCandidateRanking.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
