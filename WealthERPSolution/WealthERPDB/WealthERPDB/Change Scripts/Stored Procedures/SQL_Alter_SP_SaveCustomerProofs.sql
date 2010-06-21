-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_SaveCustomerProofs]
@C_CustomerId int,
@XP_ProofCode int,
@CP_CreatedBy int,
@CP_ModifiedBy int
as
insert into CustomerProof(
C_CustomerId ,
XP_ProofCode,
CP_CreatedBy,
CP_CreatedOn,
CP_ModifiedBy,
CP_ModifiedOn
)
 values
(@C_CustomerId ,
@XP_ProofCode,
@CP_CreatedBy,
current_timestamp,
@CP_ModifiedBy,
current_timestamp)
 