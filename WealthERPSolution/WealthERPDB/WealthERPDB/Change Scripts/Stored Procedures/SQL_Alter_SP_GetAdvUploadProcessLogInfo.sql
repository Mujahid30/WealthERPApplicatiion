


-- =============================================
-- Author:		<Benson>
-- Create date: <9th June 2009>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetAdvUploadProcessLogInfo]
@processId bigint
as
SET NOCOUNT ON
SELECT [ADUL_ProcessId]
	  ,[ADUL_FileName]
      ,[XESFT_FileTypeId]
      ,[ADUL_TotalNoOfRecords]
      ,[U_UserId]
      ,[ADUL_XMLFileName]
      ,[A_AdviserId]
      ,[ADUL_Comment]
      ,[ADUL_StartTime]
      ,[ADUL_EndTime]
      ,[ADUL_NoOfCustomersCreated]
      ,[ADUL_NoOfTransactionsCreated]
      ,[ADUL_NoOfFoliosCreated]
      ,[ADUL_NoOfRejectRecords]
      ,[ADUL_IsXMLConvesionComplete]
      ,[ADUL_IsInsertionToInputComplete]
      ,[ADUL_IsInsertionToStagingComplete]
      ,[ADUL_IsInsertionToWerpComplete]
      ,[ADUL_IsInsertionToXtrnlComplete]
       
  FROM AdviserDailyUploadLog
  where [ADUL_ProcessId] = @processId
  SET NOCOUNT OFF
 