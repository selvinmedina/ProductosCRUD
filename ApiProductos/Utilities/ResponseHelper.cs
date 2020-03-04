using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Common
{
    public abstract class ResponseHelperBase
    {
        public bool Response { get; set; }
        public string Message { get; set; }
        public string Function { get; set; }
        public string Href { get; set; }
        public Dictionary<string, string> Validations { get; set; }
        public System.Net.HttpStatusCode status { get; set; }

        public void SetValidations(Dictionary<string, string> vals)
        {
            if (vals != null && vals.Count > 0)
            {
                Validations = vals;
                PrepareResponse(false, "La validación no ha sido superada");
            }
        }

        protected void PrepareResponse(bool r, string m = "", HttpStatusCode status = HttpStatusCode.OK)
        {
            Response = r;
            this.status = status;
            if (r)
            {
                Message = m;
            }
            else
            {
                Message = (m == "" ? "Ocurrió un error inesperado" : m);
            }
        }

        public ResponseHelperBase()
        {
            PrepareResponse(false);
        }
    }

    public class ResponseHelper : ResponseHelperBase
    {
        public ResponseHelper()
        {
        }

        public ResponseHelper(dynamic result, HttpStatusCode status, HttpRequestMessage request)
        {
            Result = result;
        }

        public dynamic Result { get; set; }

        public ResponseHelper SetResponse(bool r, string m = "", HttpStatusCode status = HttpStatusCode.OK)
        {
            PrepareResponse(r, m, status);
            this.status = status;
            return this;
        }
    }

    public class ResponseHelper<T> : ResponseHelperBase where T : class
    {
        public T Result { get; set; }

        public ResponseHelper<T> SetResponse(bool r, string m = "", HttpStatusCode status = HttpStatusCode.OK)
        {
            PrepareResponse(r, m, status);
            return this;
        }
    }
}
