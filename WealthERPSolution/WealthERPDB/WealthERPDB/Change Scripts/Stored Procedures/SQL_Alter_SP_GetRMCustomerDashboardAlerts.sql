ALTER PROCEDURE [dbo].SP_GetRMCustomerDashboardAlerts
@RMId INT

AS

BEGIN
	
SET NOCOUNT ON;

	(
	SELECT TOP 2
		C.C_FirstName+' '+C.C_MiddleName+' '+C.C_LastName AS CustomerName,
		EN.[AEN_SchemeID]  Details,
		EN.[AEN_EventMessage]  EventMessage,
		EL.AEL_EventCode  EventCode,
		EN.AEN_PopulatedDate  PopulatedDate,
		PASP.PASP_SchemePlanName  Name
	FROM
		dbo.AlertEventNotification AS EN
		INNER JOIN
		dbo.AlertEventLookup AS EL
		ON 
		EN.AEL_EventID = EL.AEL_EventID
		INNER JOIN
		dbo.ProductAMCSchemePlan PASP
		ON
		EN.AEN_SchemeId=PASP.PASP_SchemePlanCode
		INNER JOIN
		Customer C
		ON
		C.C_CustomerId=EN.AEN_TargetId
		
	WHERE
		
		EN.AEN_CreatedBy = @RMId
		AND
		EN.ADML_ModeId=3
		AND 
		(EL.AEL_EventCode='SIP'
		OR
		EL.AEL_EventCode='SWP')
	--ORDER BY EN.AEN_PopulatedDate DESC
	)
UNION
	
	(
	SELECT TOP 2
		C.C_FirstName+' '+C.C_MiddleName+' '+C.C_LastName AS CustomerName,
		EN.[AEN_SchemeID] Details,
		EN.[AEN_EventMessage] EventMessage,
		EL.AEL_EventCode EventCode,
		EN.AEN_PopulatedDate PopulatedDate,
		CPNP.CPNP_Name Name
	FROM
		dbo.AlertEventNotification AS EN
		INNER JOIN
		dbo.AlertEventLookup AS EL
		ON 
		EN.AEL_EventID = EL.AEL_EventID
		INNER JOIN
		dbo. CustomerPropertyNetPosition CPNP
		ON
		EN.AEN_SchemeId=CPNP.CPNP_PropertyNPId
		INNER JOIN
		Customer C
		ON
		C.C_CustomerId=EN.AEN_TargetId
		
	WHERE
		
		EN.AEN_CreatedBy = @RMId
		AND
		EN.ADML_ModeId=3
		AND 
		EL.AEL_EventCode='Property'
	--ORDER BY EN.AEN_PopulatedDate DESC
	)
UNION
	(
	SELECT TOP 2
		C.C_FirstName+' '+C.C_MiddleName+' '+C.C_LastName AS CustomerName,
		EN.[AEN_SchemeID]  Details,
		EN.[AEN_EventMessage] EventMessage,
		EL.AEL_EventCode EventCode,
		EN.AEN_PopulatedDate PopulatedDate,
		CPNP.CPNP_Name Name

	FROM
		dbo.AlertEventNotification AS EN
		INNER JOIN
		dbo.AlertEventLookup AS EL
		ON 
		EN.AEL_EventID = EL.AEL_EventID
		INNER JOIN
		dbo. CustomerPersonalNetPosition CPNP
		ON
		EN.AEN_SchemeId=CPNP.CPNP_PersonalNPId
		INNER JOIN
		Customer C
		ON
		C.C_CustomerId=EN.AEN_TargetId
		
	WHERE
		
		EN.AEN_CreatedBy = @RMId
		AND
		EN.ADML_ModeId=3
		AND 
		EL.AEL_EventCode='Personal'
	--ORDER BY EN.AEN_PopulatedDate DESC
	)
	UNION
	(
	SELECT TOP 2
		C.C_FirstName+' '+C.C_MiddleName+' '+C.C_LastName AS CustomerName,
		''  Details,
		EN.[AEN_EventMessage] EventMessage,
		EL.AEL_EventCode EventCode,
		EN.AEN_PopulatedDate PopulatedDate,
		'' Name

	FROM
		dbo.AlertEventNotification AS EN
		INNER JOIN
		dbo.AlertEventLookup AS EL
		ON 
		EN.AEL_EventID = EL.AEL_EventID
		INNER JOIN
		Customer C
		ON
		C.C_CustomerId=EN.AEN_TargetId
		
	WHERE
		
		EN.AEN_CreatedBy = @RMId
		AND
		EN.ADML_ModeId=3
		AND 
		EL.AEL_EventCode='DOB'
	--ORDER BY EN.AEN_PopulatedDate DESC
	)

	SET NOCOUNT OFF
END


 