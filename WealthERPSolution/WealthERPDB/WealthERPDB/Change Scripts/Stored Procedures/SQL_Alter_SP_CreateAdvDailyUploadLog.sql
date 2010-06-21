

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateAdvDailyUploadLog]



@ADUL_FileName	varchar(50),
@XESFT_FileTypeId int,
@ADUL_NoOfTotalRecords int,
@U_UserId int,
@A_AdviserId	int,
@ADUL_StartTime	datetime,
@ADUL_IsXMLConvesionComplete	tinyint,
@ADUL_CreatedBy	int,
@ADUL_ModifiedBy	int,
@ADUL_ProcessId int OUTPUT

as

BEGIN
	
		INSERT INTO [AdviserDailyUploadLog]
		(
			ADUL_FileName,
			XESFT_FileTypeId,
			ADUL_TotalNoOfRecords,
			U_UserId,
			A_AdviserId,
			ADUL_StartTime,
			ADUL_IsXMLConvesionComplete,
			ADUL_CreatedBy,
			ADUL_CreatedOn,
			ADUL_ModifiedBy,
			ADUL_ModifiedOn

		) 
		VALUES
		(
			@ADUL_FileName,
			@XESFT_FileTypeId,
			@ADUL_NoOfTotalRecords,
			@U_UserId,
			@A_AdviserId,
			@ADUL_StartTime,
			@ADUL_IsXMLConvesionComplete,
			@ADUL_CreatedBy,
			CURRENT_TIMESTAMP,
			@ADUL_ModifiedBy,
			CURRENT_TIMESTAMP
		)

		SELECT @ADUL_ProcessId = SCOPE_IDENTITY()
	
END



 