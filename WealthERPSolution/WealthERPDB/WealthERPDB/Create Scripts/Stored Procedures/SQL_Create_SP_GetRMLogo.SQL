/****** Object:  StoredProcedure [dbo].[SP_GetRMLogo]    Script Date: 06/24/2009 12:04:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMLogo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetRMLogo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRMLogo]    Script Date: 06/24/2009 12:04:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMLogo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetRMLogo]
@AR_RMID BIGINT
as
SELECT A.A_ADVISERLOGO FROM dbo.Adviser A,dbo.AdviserRM R WHERE A.A_ADVISERID=R.A_ADVISERID AND R.AR_RMID=@AR_RMID

' 
END
GO
