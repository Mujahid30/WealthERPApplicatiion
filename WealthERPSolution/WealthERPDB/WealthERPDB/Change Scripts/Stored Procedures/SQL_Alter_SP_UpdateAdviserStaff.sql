-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateAdviserStaff]
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
 