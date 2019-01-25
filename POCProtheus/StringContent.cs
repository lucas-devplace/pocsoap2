using System.Text;

namespace POCProtheus
{
    internal class StringContent
    {
        private object p;
        private Encoding uTF8;
        private string v;

        public StringContent(object p, Encoding uTF8, string v)
        {
            this.p = p;
            this.uTF8 = uTF8;
            this.v = v;
        }
    }
}