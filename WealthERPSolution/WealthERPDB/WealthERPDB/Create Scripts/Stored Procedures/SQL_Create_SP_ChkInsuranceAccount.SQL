/****** Object:  StoredProcedure [dbo].[SP_ChkInsuranceAccount]    Script Date: 06/24/2009 11:03:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ChkInsuranceAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_ChkInsuranceAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_ChkInsuranceAccount]    Script Date: 06/24/2009 11:03:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ChkInsuranceAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_ChkInsuranceAccount]
@C_CustomerId INT
AS
SELECT 
	COUNT(*) 
FROM 
	dbo.CustomerInsuranceAccount as CIA
	INNER JOIN
	CustomerPortfolio AS CP
	ON CIA.CP_PortfolioId = CP.CP_PortfolioId
WHERE
	CP.C_CustomerId=@C_CustomerId
' 
END
GO
