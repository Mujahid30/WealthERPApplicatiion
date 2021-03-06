/****** Object:  StoredProcedure [dbo].[SP_CreateRMUser]    Script Date: 06/24/2009 11:14:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateRMUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateRMUser]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateRMUser]    Script Date: 06/24/2009 11:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateRMUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateRMUser]
@U_UserId int,
@U_Password varchar(50),
@U_FirstName varchar(50),
@U_MiddleName varchar(50),
@U_Lastname varchar(50),
@U_Email varchar(MAX),
@U_UserType varchar(10)

as

insert into dbo.[User]
(
U_UserId,
U_Password,
U_FirstName,
U_MiddleName,
U_Lastname,
U_Email,
U_UserType)
 values(
 @U_UserId,
 @U_Password,
 @U_FirstName,
 @U_MiddleName,
 @U_LastName,
 @U_Email,
 @U_UserType)
' 
END
GO
