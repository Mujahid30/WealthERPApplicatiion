/****** Object:  StoredProcedure [dbo].[SP_GetUser]    Script Date: 06/24/2009 12:05:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetUser]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUser]    Script Date: 06/24/2009 12:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetUser]
@U_LoginId varchar(50)
as
select * from [User] where U_LoginId=@U_LoginId
' 
END
GO
