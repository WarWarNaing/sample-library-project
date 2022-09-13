﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Extensions.Compression.Client;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class baseUrls
    {
        public static String API_URL = "http://localhost:62289/";
    }
    public class ApiRequest<T, U>
    {
        private static String API_URL = baseUrls.API_URL;
        public static async Task<U> PostDiffRequest(string url, T entity, bool isCompressed = true)
        {
            url = API_URL + url;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Accept.Clear();

                using (var content = new StringContent(JsonConvert.SerializeObject(entity), UTF8Encoding.UTF8, "application/json"))
                {
                    using (var response = await client.PostAsync(url, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var objsJsonString = await response.Content.ReadAsStringAsync();
                            var jsonContent = JsonConvert.DeserializeObject<U>(objsJsonString);
                            return jsonContent;
                        }
                        else
                        {
                            return default(U);
                        }
                    }
                }
            }
        }

    }

    public class ApiRequest<T>
    {
        private static String API_URL = baseUrls.API_URL;

        public static string Get(string url, out T entity)
        {

            try
            {
                url = API_URL + url;
                entity = default(T);
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type:application/json"); //Content-Type  
                    client.Headers.Add("Accept:application/json");
                    var result = client.DownloadString(url); //URI 
                    if (result != null)
                    {
                        entity = JsonConvert.DeserializeObject<T>(result);
                        return "SUCCESS";
                    }
                    else
                    {
                        return "FAIL";
                    }
                }
            }
            catch (WebException ex)
            {
                entity = default(T);
                return "E01";
            }
            catch (Exception ex)
            {
                entity = default(T);
                return "E02";
            }
        }
        
        public static async Task<T> GetRequest(string url, bool isCompressed = false, string rootUrl = null)
        {

            if (rootUrl != null)
            {
                API_URL = rootUrl;
            }
            url = API_URL + url;
            try
            {
                if (isCompressed)
                {
                    using (var client = new HttpClient(new ClientCompressionHandler(new GZipCompressor(), new DeflateCompressor())))
                    {
                        client.BaseAddress = new Uri(API_URL);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                        using (var response = await client.GetAsync(url))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var objsJsonString = await response.Content.ReadAsStringAsync();
                                var jsonContent = JsonConvert.DeserializeObject<T>(objsJsonString);
                                return jsonContent;
                            }
                            else
                            {
                                return default(T);
                            }
                        }
                    }
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(API_URL);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        using (var response = await client.GetAsync(url))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var objsJsonString = await response.Content.ReadAsStringAsync();
                                var jsonContent = JsonConvert.DeserializeObject<T>(objsJsonString);
                                return jsonContent;
                            }
                            else
                            {
                                return default(T);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex;
                return default(T);
            }
        }
        public static async Task<T> DeleteRequest(string url)
        {
            url = API_URL + url;
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(API_URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await client.DeleteAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var objsJsonString = await response.Content.ReadAsStringAsync();
                            var jsonContent = JsonConvert.DeserializeObject<T>(objsJsonString);
                            return jsonContent;
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }

            }
            catch
            {
                return default(T);
            }
        }

        public static async Task<T> PostRequest(string url, T entity, string rootUrl = null)
        {

            if (rootUrl != null)
            {
                API_URL = rootUrl;
            }
            url = API_URL + url;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var content = new StringContent(JsonConvert.SerializeObject(entity), UTF8Encoding.UTF8, "application/json"))
                {
                    using (var response = await client.PostAsync(url, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var objsJsonString = await response.Content.ReadAsStringAsync();
                            var jsonContent = JsonConvert.DeserializeObject<T>(objsJsonString);
                            return jsonContent;
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
            }
        }
    }
}