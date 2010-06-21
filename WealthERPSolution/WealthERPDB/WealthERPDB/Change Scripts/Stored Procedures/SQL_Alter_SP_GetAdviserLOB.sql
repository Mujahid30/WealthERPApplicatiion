 -- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAdviserLOB]
@A_AdviserId int,
@NameFilter varchar(100) = null,
@BTypeFilter varchar(100) = null

as

SET NOCOUNT ON
	
	if (@NameFilter is null AND @BTypeFilter is null)
	begin /*If no filter criteria is selected*/
	
		select 
			AL_LOBId,
			AL_OrgName,
			XALC_LOBClassificationCode,
			XALIT_IdentifierTypeCode,
			AL_Identifier
		from 
			AdviserLOB
		where 
			A_AdviserId = @A_AdviserId
			
	
	end
	else if (@NameFilter is not null AND @BTypeFilter is null)
	begin /*If only Business Type filter criteria is selected*/
		select 
			AL_LOBId,
			AL_OrgName,
			XALC_LOBClassificationCode,
			XALIT_IdentifierTypeCode,
			AL_Identifier
		from 
			AdviserLOB
		where 
			A_AdviserId = @A_AdviserId
			and
			AL_OrgName like '%'+@NameFilter+'%'
	end
	else if (@NameFilter is null AND @BTypeFilter is not null)
	begin /*If only Name filter criteria is selected*/
		select 
			AL_LOBId,
			AL_OrgName,
			XALC_LOBClassificationCode,
			XALIT_IdentifierTypeCode,
			AL_Identifier
		from 
			AdviserLOB
		where 
			A_AdviserId = @A_AdviserId
			and
			XALC_LOBClassificationCode like '%'+@BTypeFilter+'%'
	end
	else if (@NameFilter is not null AND @BTypeFilter is not null)
	begin /*If both filter criteria are selected*/
	select 
			AL_LOBId,
			AL_OrgName,
			XALC_LOBClassificationCode,
			XALIT_IdentifierTypeCode,
			AL_Identifier
		from 
			AdviserLOB
		where 
			A_AdviserId = @A_AdviserId
			and
			AL_OrgName like '%'+@NameFilter+'%'
			and
			XALC_LOBClassificationCode like '%'+@BTypeFilter+'%'
	end
	
	select 
		distinct XALC_LOBClassificationCode
	from
		AdviserLOB
	where A_AdviserId = @A_AdviserId
	

SET NOCOUNT OFF 
