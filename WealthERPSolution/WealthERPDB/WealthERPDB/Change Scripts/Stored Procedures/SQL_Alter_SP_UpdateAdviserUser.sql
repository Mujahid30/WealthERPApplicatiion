-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateAdviserUser]
@A_AdviserId int,
@U_UserId int,
@A_OrgName varchar(25),
@A_AddressLine1 varchar(25),
@A_AddressLine2 varchar(25),
@A_AddressLine3 varchar(25),
@A_City	varchar(25),
@A_State	varchar(25),
@A_PinCode	numeric(6, 0),
@A_Country	varchar(25),
@A_Phone1STD	numeric(4, 0),
@A_Phone1ISD	numeric(4, 0),
@A_Phone1Number	numeric(8, 0),
@A_Phone2STD	numeric(4, 0),
@A_Phone2ISD	numeric(4, 0),
@A_Phone2Number	numeric(8, 0),
@A_Email	varchar(50),
@A_FAXISD	numeric(4, 0),
@A_FAXSTD	numeric(4, 0),
@A_FAX	numeric(8, 0),
@XABT_BusinessTypeCode	varchar(5),
@A_ContactPersonFirstName	varchar(25),
@A_ContactPersonMiddleName	varchar(25),
@A_ContactPersonLastName	varchar(25),
@A_ContactPersonMobile	numeric(10, 0),
@A_IsMultiBranch	TINYINT

as

update Adviser set A_OrgName=@A_OrgName ,
						 A_AddressLine1=@A_AddressLine1,
						 A_AddressLine2=@A_AddressLine2,
						 A_AddressLine3=@A_AddressLine3, 	
						 A_City=@A_City,
						 A_State=@A_State,
						 A_PinCode=@A_PinCode,
						 A_Country=@A_Country,
						 A_Phone1STD=@A_Phone1STD,
						 A_Phone1ISD=@A_Phone1ISD,
						 A_Phone1Number=@A_Phone1Number,
						 A_Phone2STD=@A_Phone2STD,
						 A_Phone2ISD=@A_Phone2ISD,
						 A_Phone2Number=@A_Phone2Number,
						 A_Email=@A_Email,
						 A_FAXISD=@A_FAXISD,
						 A_FAXSTD=@A_FAXSTD,
						 A_FAX=@A_FAX,
						 XABT_BusinessTypeCode=@XABT_BusinessTypeCode,
						 A_ContactPersonFirstName=@A_ContactPersonFirstName,
						 A_ContactPersonMiddleName=@A_ContactPersonMiddleName,
						 A_ContactPersonLastName=@A_ContactPersonLastName,
						 A_ContactPersonMobile=@A_ContactPersonMobile,
						 A_IsMultiBranch=@A_IsMultiBranch

						 where A_AdviserId=@A_AdviserId and U_UserId=@U_UserId

--go

--execute updateadviseruser;
 