﻿namespace Models.Check.Equipment
{
    public class QueryFormModel
    {
        public QueryParameters QueryParameters { get; set; }

        public QueryFormModel()
        {
            QueryParameters = new QueryParameters();
        }
    }
}
