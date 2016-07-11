namespace Dealership.Models
{
    using System;
    using System.Text;

    using Dealership.Contracts;
    using Common;

    internal class Comment : IComment
    {
        private string authorUsername;
        private string content;

        public Comment(string content)
        {
            this.Content = content;
        }

        public string Author
        {
            get
            {
                return this.authorUsername;
            }

            set
            {
                Validator.ValidateNull(value, Constants.UserCannotBeNull);

                this.authorUsername = value;
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }

            protected set
            {
                Validator.ValidateNull(value, Constants.CommentCannotBeNull);

                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinCommentLength,
                    Constants.MaxCommentLength,
                    string.Format(
                        Constants.StringMustBeBetweenMinAndMax,
                        "Content",
                        Constants.MinCommentLength,
                        Constants.MaxCommentLength));

                this.content = value;
            }
        }

        /// <summary>
        /// 4 spaces indentation
        /// ----------
        ///  {Content}
        ///    User: {Comment Username}
        /// ----------
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var outputbuilder = new StringBuilder();
            var commentBorder = "    ----------";

            outputbuilder.AppendLine(commentBorder);
            outputbuilder.AppendLine(string.Format("    {0}" ,this.Content));
            outputbuilder.AppendLine(string.Format("      User: {0}", this.Author));
            outputbuilder.Append(commentBorder);

            return outputbuilder.ToString();
        }
    }
}
