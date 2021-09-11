namespace InternetMessage.Reader
{
    public class InternetMessageBody: InternetMessageNode
    {
        public override InternetMessageNodeType Type => InternetMessageNodeType.Body;

        private string v;

        public InternetMessageBody(string v)
        {
            this.v = v;
        }
    }
}