using System;
using System.Collections.Generic;
using System.Text;
using DAL.DAO;

namespace BAL.Abstract
{
    public  interface ITwitterService
    {
        System.Threading.Tasks.Task ReadTweets(Status status);
    }
}
