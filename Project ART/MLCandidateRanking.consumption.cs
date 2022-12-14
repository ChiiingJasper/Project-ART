// This file was auto-generated by ML.NET Model Builder.
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
            [ColumnName(@"DISC_Personality")]
            public string DISC_Personality { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"Resume_Score")]
            public float Resume_Score { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"Approved")]
            public float Approved { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLCandidateRanking.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"DISC_Personality")]
            public float[] DISC_Personality { get; set; }

            [ColumnName(@"Resume_Score")]
            public float Resume_Score { get; set; }

            [ColumnName(@"Approved")]
            public uint Approved { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public float PredictedLabel { get; set; }

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
