-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateBranchTerminal]
@AT_TerminalId NUMERIC(10,0),
@AB_BranchId	int	,
@AT_CreatedBy	int,	
@AT_ModifiedBy	int	

as

insert into AdviserTerminal
(
AT_TerminalId,
AB_BranchId,
AT_CreatedBy,
AT_CreatedOn,
AT_ModifiedBy,
AT_ModifiedOn


)
 values
(
@AT_TerminalId,
@AB_BranchId,
@AT_CreatedBy,
current_timestamp,
@AT_ModifiedBy,
current_timestamp

)
 