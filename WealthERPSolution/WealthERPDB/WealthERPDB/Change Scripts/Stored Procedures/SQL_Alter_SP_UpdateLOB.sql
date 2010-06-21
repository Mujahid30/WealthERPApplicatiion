-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateLOB]
@AL_LOBId	int,
@AL_OrgName	varchar(25),
@AL_Identifier	varchar(25),
@XALC_LOBClassificationCode	varchar(20),
@AL_LicenseNo	varchar(50),
@AL_Validity	datetime
	
as

update AdviserLOB set

AL_OrgName=@AL_OrgName,
AL_Identifier=@AL_Identifier,
XALC_LOBClassificationCode=@XALC_LOBClassificationCode,
AL_LicenseNo=@AL_LicenseNo,
AL_Validity=@AL_Validity

where AL_LOBId=@AL_LOBId 