using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace busroutes
{
	public class RequestState
	{
		// This class stores the state of the request. 
		const int BUFFER_SIZE = 1024;
		public StringBuilder requestData;
		public byte[] bufferRead;
		public WebRequest request;
		public WebResponse response;
		public Stream responseStream;
		public String content { get; set;}
		public String postParam;
		public RequestState(WebRequest request, String postparam)
		{
			bufferRead = new byte[BUFFER_SIZE];
			requestData = new StringBuilder("");
			this.request = request;
			responseStream = null;
			postParam = postparam;
		}
	}
	class  BusRoutesWebRequest
	{

		const string BASE_URL = "https://api.appglu.com/v1/queries";
		const string username = "WKD4N7YMA1uiM8V";
		const string password = "DtdTtzMLQlA0hk2C1Yi5pLyVIlAQ68";

		public ManualResetEvent allDone= new ManualResetEvent(false);
		const int BUFFER_SIZE = 1024;

		public String Execute(String path, String query){
			try
			{
				
				WebRequest request= WebRequest.Create (BASE_URL + path);

				request.ContentType = "application/json";
				request.Method = "POST";

				request.Credentials = new NetworkCredential (username, password);
				request.Headers["X-AppGlu-Environment"] = "staging";

				RequestState requestState = new RequestState(request, query);

				request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), requestState);

				allDone.WaitOne();
				allDone.Dispose();

				requestState.response.Dispose();
				return requestState.content ;

			}
			catch(WebException e)
			{
				Debug.WriteLine("WebException raised!");
				Debug.WriteLine("\n{0}",e.Message);
				Debug.WriteLine("\n{0}",e.Status);
			} 
			catch(Exception e)
			{
				Debug.WriteLine("Exception raised!");
				Debug.WriteLine("Source : " + e.Source);
				Debug.WriteLine("Message : " + e.Message);
			}

			return null;
		}

		private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{
			RequestState requestState = (RequestState)asynchronousResult.AsyncState;
			WebRequest request = requestState.request;
			// End the stream request operation

			Stream postStream = request.EndGetRequestStream(asynchronousResult);

			// Create the post data
			string postData = requestState.postParam;

			byte[] byteArray = Encoding.UTF8.GetBytes(postData);

			postStream.Write(byteArray, 0, byteArray.Length);
			postStream.Dispose();

			request.BeginGetResponse(new AsyncCallback(RespCallback),requestState);

		}

		private void RespCallback(IAsyncResult asynchronousResult)
		{  
			try
			{
				// Set the State of request to asynchronous.
				RequestState requestState = (RequestState) asynchronousResult.AsyncState;
				WebRequest  request =requestState.request;
				// End the Asynchronous response.
				requestState.response =  request.EndGetResponse(asynchronousResult);
				// Read the response into a 'Stream' object.
				Stream responseStream = requestState.response.GetResponseStream();
				requestState.responseStream = responseStream;

				ReadCallBack(requestState);
			}
			catch(WebException e)
			{
				Debug.WriteLine("WebException raised!");
				Debug.WriteLine("\n{0}",e.Message);
				Debug.WriteLine("\n{0}",e.Status);
			} 
			catch(Exception e)
			{
				Debug.WriteLine("Exception raised!");
				Debug.WriteLine("Source : " + e.Source);
				Debug.WriteLine("Message : " + e.Message);
			}
		}
		private void ReadCallBack(RequestState requestState)
		{
			try
			{
				Stream responseStream = requestState.responseStream;
				int read = responseStream.Read( requestState.bufferRead,0, BUFFER_SIZE);
				// Read the contents of the HTML page and then print to the console. 
				if (read > 0)
				{
					requestState.requestData.Append(Encoding.UTF8.GetString(requestState.bufferRead, 0, read));
					ReadCallBack(requestState);
				}
				else
				{
					Debug.WriteLine("\nThe HTML page Contents are:  ");
					if(requestState.requestData.Length>1)
					{
						string stringContent = requestState.requestData.ToString();
						requestState.content = stringContent;

					}
					Debug.WriteLine("\nPress 'Enter' key to continue........");
					responseStream.Dispose();
					allDone.Set();
				}
			}
			catch(WebException e)
			{
				Debug.WriteLine("WebException raised!");
				Debug.WriteLine("\n{0}",e.Message);
				Debug.WriteLine("\n{0}",e.Status);
			} 
			catch(Exception e)
			{
				Debug.WriteLine("Exception raised!");
				Debug.WriteLine("Source : {0}" , e.Source);
				Debug.WriteLine("Message : {0}" , e.Message);
			}

		}
	}
}

