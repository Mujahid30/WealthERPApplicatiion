/****** Object:  StoredProcedure [dbo].[SP_GetAdviserUserInfo]    Script Date: 06/24/2009 11:57:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserUserInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserUserInfo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserUserInfo]    Script Date: 06/24/2009 11:57:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserUserInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAdviserUserInfo]
@A_AdviserId BIGINT
as
select * from [User] INNER JOIN dbo.Adviser ON [User].U_UserId=dbo.Adviser.U_UserId where dbo.Adviser.A_AdviserId=@A_AdviserId
' 
END
GO
