/****** Object:  StoredProcedure [dbo].[SP_UpdateUser]    Script Date: 06/24/2009 12:07:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateUser]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUser]    Script Date: 06/24/2009 12:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateUser]
@U_UserId INT,
@U_FirstName varchar(50),
@U_MiddleName varchar(50),
@U_LastName varchar(50),
@U_Email varchar(MAX),
@U_LoginId varchar(MAX),
@U_Password varchar(50)


as

update [User] set 
U_FirstName=@U_FirstName,
U_MiddleName=@U_MiddleName,
U_LastName=@U_LastName,
U_Email=@U_Email,
U_LoginId=@U_LoginId,
U_Password=@U_Password 
where 
U_UserId=@U_UserId
' 
END
GO
