/****** Object:  StoredProcedure [dbo].[SP_UpdateAdviserStaff]    Script Date: 06/24/2009 12:06:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateAdviserStaff]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateAdviserStaff]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAdviserStaff]    Script Date: 06/24/2009 12:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateAdviserStaff]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateAdviserStaff]
@AR_RMId int,
@AR_FirstName varchar(50),
@AR_MiddleName varchar(50),
@AR_LastName varchar(50),
@AR_OfficePhoneDirectISD numeric(4,0),
@AR_OfficePhoneDirectSTD numeric(4,0),
@AR_OfficePhoneDirect numeric(8,0),
@AR_OfficePhoneExtISD numeric(4,0),
@AR_OfficePhoneExtSTD numeric(4,0),
@AR_OfficePhoneExt numeric(8,0),
@AR_ResPhoneISD numeric(4,0),
@AR_ResPhoneSTD numeric(4,0),
@AR_ResPhone numeric(8,0),
@AR_Mobile numeric(10,0),
@AR_FaxISD numeric(4,0),
@AR_FaxSTD numeric(4,0),
@AR_Fax numeric(8,0),
@AR_Email varchar(50)

as

update AdviserRM SET
 AR_FirstName=@AR_FirstName,
 AR_MiddleName=@AR_MiddleName,
 AR_LastName=@AR_LastName,
 AR_OfficePhoneDirectISD=@AR_OfficePhoneDirectISD,
 AR_OfficePhoneDirectSTD=@AR_OfficePhoneDirectSTD,
 AR_OfficePhoneDirect=@AR_OfficePhoneDirect,
 AR_OfficePhoneExtISD=@AR_OfficePhoneExtISD,
 AR_OfficePhoneExtSTD=@AR_OfficePhoneExtSTD,
 AR_OfficePhoneExt=@AR_OfficePhoneExt,
 AR_ResPhoneISD=@AR_ResPhoneISD,
 AR_ResPhoneSTD=@AR_ResPhoneSTD,
 AR_ResPhone=@AR_ResPhone,
 AR_Mobile=@AR_Mobile,
 AR_FaxISD=@AR_FaxISD,
 AR_FaxSTD=@AR_FaxSTD,
 AR_Fax=@AR_Fax,
 AR_Email=@AR_Email
  WHERE
   AR_RMId=@AR_RMId
' 
END
GO
