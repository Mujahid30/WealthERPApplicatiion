/****** Object:  StoredProcedure [dbo].[SP_GetRMAdviserId]    Script Date: 06/24/2009 12:04:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMAdviserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetRMAdviserId]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRMAdviserId]    Script Date: 06/24/2009 12:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMAdviserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetRMAdviserId


@AR_RMId INT

AS


SELECT 
	A_AdviserId 
FROM 
	AdviserRM
WHERE AR_RMId=@AR_RMId' 
END
GO
