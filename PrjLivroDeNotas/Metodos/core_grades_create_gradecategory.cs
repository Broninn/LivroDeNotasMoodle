using Newtonsoft.Json;
using PrjLivroDeNotas.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrjLivroDeNotas.Metodos
{
    public class core_grades_create_gradecategory
    {
        private String function = "&wsfunction=core_grades_create_gradecategories&moodlewsrestformat=json";

        public string CoreGradesCreateGradeCategory(String pParams)
        {
            var ret = new Conexao().GetConnectionPRODUCAO() + function + pParams;
            return ret;
        }


        public async Task<Retorno> SetCategorisInfo(CategoryCreateParams pCategory)
        {            
            using (var client = new HttpClient())
            {
                var g = CoreGradesCreateGradeCategory(GetCategoryParams(pCategory));
                using (var response = await client.GetAsync(g))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProgressJsonString = await response.Content.ReadAsStringAsync();
                        Retorno retorno = JsonConvert.DeserializeObject<Retorno>(ProgressJsonString);                        

                        return retorno;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public string GetCategoryParams(CategoryCreateParams pCategoryCreateParams)
        {
            PropertyInfo[] properties = pCategoryCreateParams.GetType().GetProperties();
              
            String valueParams = null;

            foreach (var propertyInfo in properties)

                if (propertyInfo.Name != "courseid" & propertyInfo.Name != "fullname")
                {
                    if (propertyInfo.GetValue(pCategoryCreateParams, null) != null)
                    {
                        valueParams += $"&categories[0][options][{propertyInfo.Name}]={propertyInfo.GetValue(pCategoryCreateParams, null)}";
                    }
                }
                else if(propertyInfo.Name == "fullname")
                {
                    valueParams += $"&categories[0][{propertyInfo.Name}]={propertyInfo.GetValue(pCategoryCreateParams, null)}";
                }
                else
                {
                    valueParams += $"&{propertyInfo.Name}={propertyInfo.GetValue(pCategoryCreateParams, null)}";
                }
            return valueParams;
        }
        public class Retorno
        {
            public List<int> categoryids { get; set; }
            public List<object> warnings { get; set; }
        }


        public class CategoryCreateParams
        {
            public int courseid { get; set; }
            public String fullname { get; set; }
            public int aggregation { get; set; }
            public int aggregateonlygraded { get; set; }= 1 ;
            public int aggregateoutcomes { get; set; }
            public int droplow { get; set; } 
            public string itemname { get; set; } 
            public string iteminfo { get; set; } = null;
            public string idnumber { get; set; }
            public int gradetype { get; set; } = 1;
            public int grademax { get; set; }
            public int grademin { get; set; } 
            public int gradepass { get; set; }
            public int display { get; set; } = 0;
            public int decimals { get; set; } = 1;
            public int hiddenuntil { get; set; }
            public int locktime { get; set; }
            public int weightoverride { get; set; }
            public decimal aggregationcoef2 { get; set; }
            public int parentcategoryid { get; set; }
            public string parentcategoryidnumber { get; set; } = null;

            
        }
    }
}
