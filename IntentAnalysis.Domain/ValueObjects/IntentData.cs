using Microsoft.ML.Runtime.Api;

namespace IntentAnalysis.Domain.ValueObjects
{
    public class IntentData
    {
        [Column("0")]
        public string Text;
        [Column("1")]
        [ColumnName("Label")]
        public string Label;
    }
}
