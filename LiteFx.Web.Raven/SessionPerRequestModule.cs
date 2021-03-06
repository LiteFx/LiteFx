﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LiteFx.Raven;

namespace LiteFx.Web.Raven
{
    /// <summary>
    /// Session per request Module.
    /// Handles the NHibernate session when the request ends.
    /// </summary>
    public class SessionPerRequestModule : IHttpModule
    {
        /// <summary>
        /// Dispose the module.
        /// In this module nothing needs to be diposed.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Sing the HttpAplication request events.
        /// </summary>
        /// <param name="context">HttpApplication context</param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;

            if (application.Context.Error == null)
            {
                try
                {
                    SessionFactoryManager.Current.CommitTransaction();
                }
                catch
                {
                    SessionFactoryManager.Current.RollbackTransaction();
                    throw;
                }
                finally
                {
                    SessionFactoryManager.Current.DisposeSession();
                }
            }
            else
            {
                SessionFactoryManager.Current.RollbackTransaction();
                SessionFactoryManager.Current.DisposeSession();
            }
        }
    }
}
