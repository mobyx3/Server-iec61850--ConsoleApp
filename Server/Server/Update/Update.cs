﻿using IEC61850.Common;
using IEC61850.Server;
using ServerLib.DataClasses;

namespace ServerLib.Update
{
	public static class UpdateServer
	{
		public static void InitUpdate(IedServer iedServer, IedModel iedModel)
		{
			foreach (var item in UpdateDataObj.UpdateListDestination)
			{
				item.BaseClass.InitServer(item.NameDataObj, iedServer, iedModel);
			}
		}

		public static void SetParams(IedServer iedServer, IedModel iedModel)
		{
			UpdateDataObj.SetParamsServer(iedServer, iedModel);
		}

		public static void Clear()
		{
			UpdateDataObj.SourceList?.Clear();
			UpdateDataObj.UpdateListDestination?.Clear();
		}

		public static void InitHandlers(IedServer iedServer, IedModel iedModel)
		{			
			foreach (var itemDataObject in UpdateDataObj.UpdateListDestination)
			{
				var temp = (DataObject)iedModel.GetModelNodeByShortObjectReference(itemDataObject.NameDataObj);

				if (itemDataObject.BaseClass.GetType() == typeof(SpcClass))
				{
					object tempParam = ((SpcClass)itemDataObject.BaseClass).ctlModel;
						
					iedServer.SetCheckHandler(temp,
						(controlObject, parameter, ctlVal, test, interlockCheck, connection) =>
						{
							var result = CheckHandlerResult.ACCEPTED;
							return result;
						},
						tempParam);

					iedServer.SetWaitForExecutionHandler(temp,
						(controlObject, parameter, ctlVal, test, synchroCheck) =>
						{
							var result = ControlHandlerResult.OK;
							return result;
						}, 
						tempParam);

					iedServer.SetControlHandler(temp, 
						(controlObject, parameter, ctlVal, test) =>
						{
							if (ctlVal.GetType() != MmsType.MMS_BOOLEAN)
								return ControlHandlerResult.FAILED;

							if (!test)
							{
								var tempValue = new
								{
									Key = "stVal",
									Value = ctlVal.GetBoolean()
								};
								itemDataObject.WriteValue(tempValue);
							}

							return ControlHandlerResult.OK;
						},
						tempParam);
				}
				else if (itemDataObject.BaseClass.GetType() == typeof(DpcClass))
				{
					object tempParam = ((DpcClass)itemDataObject.BaseClass).ctlModel;
						
					iedServer.SetCheckHandler(temp,
						(controlObject, parameter, ctlVal, test, interlockCheck, connection) =>
						{
							var result = CheckHandlerResult.ACCEPTED;
							return result;
						},
						tempParam);

					iedServer.SetWaitForExecutionHandler(temp,
						(controlObject, parameter, ctlVal, test, synchroCheck) =>
						{
							var result = ControlHandlerResult.OK;
							return result;
						}, 
						tempParam);

					iedServer.SetControlHandler(temp, 
						(controlObject, parameter, ctlVal, test) =>
						{
							if (ctlVal.GetType() != MmsType.MMS_BOOLEAN)
								return ControlHandlerResult.FAILED;

							if (!test)
							{
								var tempValue = new
								{
									Key = "stVal",
									Value = ctlVal.GetBoolean()
								};
								itemDataObject.WriteValue(tempValue);
							}

							return ControlHandlerResult.OK;
						},
						tempParam);
				}
				else if (itemDataObject.BaseClass.GetType() == typeof(IncClass))
				{
					object tempParam = ((IncClass)itemDataObject.BaseClass).ctlModel;
						
					iedServer.SetCheckHandler(temp,
						(controlObject, parameter, ctlVal, test, interlockCheck, connection) =>
						{
							var result = CheckHandlerResult.ACCEPTED;
							return result;
						},
						tempParam);

					iedServer.SetWaitForExecutionHandler(temp,
						(controlObject, parameter, ctlVal, test, synchroCheck) =>
						{
							var result = ControlHandlerResult.OK;
							return result;
						}, 
						tempParam);

					iedServer.SetControlHandler(temp, 
						(controlObject, parameter, ctlVal, test) =>
						{
							if (ctlVal.GetType() != MmsType.MMS_INTEGER)
								return ControlHandlerResult.FAILED;

							if (!test)
							{
								var tempValue = new
								{
									Key = "stVal",
									Value = ctlVal.ToInt32()
								};
								itemDataObject.WriteValue(tempValue);
							}

							return ControlHandlerResult.OK;
						},
						tempParam);
				}
			}
		}

		public static void InitQualityAndTime(IedServer iedServer, IedModel iedModel)
		{
			foreach (var item in UpdateDataObj.UpdateListDestination)
			{
				item.Qality(true);
			}
		}
	}
}
