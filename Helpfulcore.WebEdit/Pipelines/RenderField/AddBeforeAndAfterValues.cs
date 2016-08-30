using Sitecore.Pipelines.RenderField;

namespace Helpfulcore.WebEdit.Pipelines.RenderField
{
	public class AddBeforeAndAfterValues : Sitecore.Pipelines.RenderField.AddBeforeAndAfterValues
	{
		protected const string BeforeKey = "Before";
		protected const string AfterKey = "After";
		protected const string EnclosingTagKey = "EnclosingTag";

		public new void Process(RenderFieldArgs args)
		{
			args.Before = this.ExtractParameter(args, "Before");
			args.After = this.ExtractParameter(args, "After");
			args.EnclosingTag = this.ExtractParameter(args, "EnclosingTag");
		}

		private string ExtractParameter(RenderFieldArgs args, string key)
		{
			var parameter = string.Empty;

			if (args.Parameters.ContainsKey(key))
			{
				parameter = args.Parameters[key];
				args.Parameters.Remove(key);
			}

			return parameter;
		}
	}
}
