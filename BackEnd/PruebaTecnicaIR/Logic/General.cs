namespace PruebaTecnicaIR.Logic
{
    public class General
    {
    }
    public class ControllerAnswer
    {

        private string message;


        public string Message
        {
            get { return message; }
            set { message = value; }
        }


        private bool errorStatus;

   
        public bool ErrorStatus
        {
            get { return errorStatus; }
            set { errorStatus = value; }
        }

        private bool updateModel;

      
        public bool UpdateModel
        {
            get { return updateModel; }
            set { updateModel = value; }
        }

     
        private int id;

   
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

      
    }
}
