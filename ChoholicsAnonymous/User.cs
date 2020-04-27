using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ChoholicsAnonymous
{
    static class User
    {
        static private bool _manager;
        static private bool _provider;
        static private bool _op;
        static public string UserID { get; set; }
        
        //sets all of the values to false except for the one being set to true 
        static public bool Manager
        {
            get { return _manager; }
            set
            {
                if (value == true)
                {
                    _provider = false;
                    _op       = false;
                    _manager  = true; 
                }
                else
                {
                    _manager = value; 
                }
            }
        }

        static public bool Provider {
            get { return _provider; }
            set
            {
                if (value == true)
                {
                    _provider    = true;
                    _op          = false; 
                    _manager     = false;
                }
                else
                {
                    _provider = value; 
                }
            }
        }

        static public bool Operator
        {
            get { return _op; }
            set
            {
                if (value == true)
                {
                    _provider    = false;
                    _op          = true;
                    _manager     = false;
                }
                else
                {
                    _op = value; 
                }
            }
        }
    }
}
