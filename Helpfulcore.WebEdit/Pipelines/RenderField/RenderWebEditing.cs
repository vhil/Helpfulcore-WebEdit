using Sitecore;
using Sitecore.Pipelines.RenderField;

namespace Helpfulcore.WebEdit.Pipelines.RenderField
{
	public class RenderWebEditing : Sitecore.Pipelines.RenderField.RenderWebEditing
	{
		public new void Process(RenderFieldArgs args)
		{
			var before = args.Before;
			var after = args.After;
			var enclosingTag = args.EnclosingTag;

			args.Before = string.Empty;
			args.After = string.Empty;
			args.EnclosingTag = string.Empty;

			base.Process(args);

			if (this.IsWrappingEnabled(args))
			{
				if (!string.IsNullOrWhiteSpace(enclosingTag))
				{
					args.Result.FirstPart = string.Format("<{0}>{1}", enclosingTag, args.Result.FirstPart);
				}

				if (!string.IsNullOrWhiteSpace(before))
				{
					args.Result.FirstPart = before + args.Result.FirstPart;
				}

				if (!string.IsNullOrWhiteSpace(enclosingTag))
				{
					args.Result.LastPart = string.Format("{0}</{1}>", args.Result.LastPart, enclosingTag);
				}

				if (!string.IsNullOrWhiteSpace(after))
				{
					args.Result.LastPart = args.Result.LastPart + after;
				}
			}
		}

		protected bool IsWrappingEnabled(RenderFieldArgs args)
		{
			return !string.IsNullOrWhiteSpace(args.FieldValue)
				   || Context.PageMode.IsPageEditor
				   || Context.PageMode.IsPageEditorEditing;
		}
	}
}