/****** Object:  StoredProcedure [dbo].[SP_UpdateLOB]    Script Date: 06/24/2009 12:07:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateLOB]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateLOB]    Script Date: 06/24/2009 12:07:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateLOB]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateLOB]
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

where AL_LOBId=@AL_LOBId' 
END
GO
