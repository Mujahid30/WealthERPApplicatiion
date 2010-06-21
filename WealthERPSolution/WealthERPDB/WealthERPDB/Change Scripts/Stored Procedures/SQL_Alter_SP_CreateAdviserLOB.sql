

ALTER PROCEDURE [dbo].[SP_CreateAdviserLOB]
@A_AdviserId bigint,
@AL_OrgName varchar(25),
@AL_Identifier varchar(25),
@XALC_LOBClassificationCode varchar(5),
@XALIT_IdentifierTypeCode VARCHAR(5),
@AL_LicenseNo varchar(50),
@AL_Validity datetime,
@AL_CreatedBy	bigint	,
@AL_ModifiedBy	bigint	


as

insert into AdviserLOB
(

A_AdviserId,
AL_OrgName,
AL_Identifier,
XALC_LOBClassificationCode,
XALIT_IdentifierTypeCode,
AL_LicenseNo,
AL_Validity,
AL_CreatedBy,
AL_CreatedOn,
AL_ModifiedBy,
AL_ModifiedOn

)
 values
(
@A_AdviserId,
@AL_OrgName,
@AL_Identifier,
@XALC_LOBClassificationCode,
@XALIT_IdentifierTypeCode,
@AL_LicenseNo,
@AL_Validity,
@AL_CreatedBy,
current_timestamp,
@AL_ModifiedBy,
current_timestamp
)
 