-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateAdviserBranch]
@A_AdviserId INT,
@AB_BranchCode varchar(10),
@AB_BranchName varchar(25),
@AB_AddressLine1 varchar(25),
@AB_AddressLine2 varchar(25),
@AB_AddressLine3 varchar(25),
@AB_Pincode numeric(6,0),
@AB_City varchar(25),
@AB_State varchar(25),
@AB_Country varchar(25),
@AB_BranchHeadId INT,
@AB_BranchHeadMobile NUMERIC(10,0),
@AB_Email varchar(50),
@AB_Phone1ISD numeric(4,0),
@AB_Phone1STD numeric(4,0),
@AB_Phone1 numeric(8,0),
@AB_Phone2ISD numeric(4,0),
@AB_Phone2STD numeric(4,0),
@AB_Phone2 numeric(8,0),
@AB_FaxISD numeric(4,0),
@AB_FaxSTD numeric(4,0),
@AB_Fax numeric(8,0),
@AB_CreatedBy	INT,
@AB_ModifiedBy	INT,
@BranchId INT OUTPUT


AS

insert into AdviserBranch 
( 
A_AdviserId,
AB_BranchCode,
AB_BranchName,
AB_AddressLine1,
AB_AddressLine2,
AB_AddressLine3,
AB_City,
AB_PinCode,
AB_State,
AB_Country,
AB_BranchHeadId,
AB_BranchHeadMobile,
AB_Email,
AB_Phone1ISD,
AB_Phone2ISD,
AB_Phone1STD,
AB_Phone1,
AB_Phone2STD,
AB_Phone2,
AB_FaxISD,
AB_Fax,
AB_FaxSTD,
AB_CreatedBy	,
AB_CreatedOn	,
AB_ModifiedOn	,
AB_ModifiedBy	
)
values (
@A_AdviserId,
@AB_BranchCode,
@AB_BranchName,
@AB_AddressLine1,
@AB_AddressLine2,
@AB_AddressLine3,
@AB_City,
@AB_PinCode,
@AB_State,
@AB_Country,

@AB_BranchHeadId,
@AB_BranchHeadMobile,
@AB_Email,
@AB_Phone1ISD,
@AB_Phone2ISD,
@AB_Phone1STD,
@AB_Phone1,
@AB_Phone2STD,
@AB_Phone2,
@AB_FaxISD,
@AB_Fax,
@AB_FaxSTD,
@AB_CreatedBy	,
current_timestamp,
current_timestamp,
@AB_ModifiedBy	
) 
SELECT @BranchId=SCOPE_IDENTITY()-- AS [SCOPE_IDENTITY]
 