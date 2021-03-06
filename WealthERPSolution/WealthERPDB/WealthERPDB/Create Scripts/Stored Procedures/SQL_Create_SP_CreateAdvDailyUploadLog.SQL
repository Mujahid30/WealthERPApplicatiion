/****** Object:  StoredProcedure [dbo].[SP_CreateAdvDailyUploadLog]    Script Date: 06/24/2009 11:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateAdvDailyUploadLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateAdvDailyUploadLog]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateAdvDailyUploadLog]    Script Date: 06/24/2009 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateAdvDailyUploadLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateAdvDailyUploadLog]



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



' 
END
GO
