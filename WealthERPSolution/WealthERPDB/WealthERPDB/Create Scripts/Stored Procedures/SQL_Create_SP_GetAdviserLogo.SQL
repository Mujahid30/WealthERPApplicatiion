/****** Object:  StoredProcedure [dbo].[SP_GetAdviserLogo]    Script Date: 06/24/2009 11:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserLogo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserLogo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserLogo]    Script Date: 06/24/2009 11:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserLogo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetAdviserLogo]
@A_AdviserId int
as
SELECT A_ADVISERLOGO FROM dbo.Adviser WHERE A_ADVISERID=@A_AdviserId
' 
END
GO
