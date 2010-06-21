-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateAdviserEODLog]


@ADEL_ProcessDate	DATETIME,

@ADEL_IsValuationComplete	TINYINT,
@ADEL_IsEquityCleanUpComplete	TINYINT,
@ADEL_AssetGroup VARCHAR(50),
@ADEL_CreatedBy	INT,
@ADEL_ModifiedBy	INT,
@A_AdviserId	INT 
	
AS

INSERT INTO AdviserDailyEODLog
(

ADEL_ProcessDate,
ADEL_StartTime,
ADEL_IsValuationComplete,
ADEL_IsEquityCleanUpComplete,
ADEL_AssetGroup,
ADEL_EndTime,
ADEL_CreatedBy,
ADEL_CreatedOn,
ADEL_ModifiedBy,
ADEL_ModifiedOn,
A_AdviserId
)
VALUES
(

@ADEL_ProcessDate,
CURRENT_TIMESTAMP,
@ADEL_IsValuationComplete,
@ADEL_IsEquityCleanUpComplete,
@ADEL_AssetGroup,
CURRENT_TIMESTAMP,
@ADEL_CreatedBy,
CURRENT_TIMESTAMP,
@ADEL_ModifiedBy,
CURRENT_TIMESTAMP,
@A_AdviserId

)
 