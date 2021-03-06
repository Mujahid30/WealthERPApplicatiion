/****** Object:  StoredProcedure [dbo].[SP_GetEquityScripPrice]    Script Date: 06/24/2009 12:02:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetEquityScripPrice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetEquityScripPrice]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEquityScripPrice]    Script Date: 06/24/2009 12:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetEquityScripPrice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE procedure [dbo].[SP_GetEquityScripPrice]  
@PEM_ScripCode int,  
@PEP_Date datetime  
as  

/*
select PEP_ClosePrice from ProductEquityPrice   
where PEM_ScripCode=@PEM_ScripCode   
and PEP_Date=(select Max(PEP_Date) from ProductEquityPrice   
where PEP_Date<@PEP_Date or PEP_Date=@PEP_Date)  
  */

SELECT Top 1 PEP.PEP_ClosePrice
FROM ProductEquityPrice PEP
WHERE 
(
	PEP.PEM_ScripCode = @PEM_ScripCode AND
	PEP.PEP_Date <=@PEP_Date
)

ORDER BY PEP.PEP_Date Desc


--exec [SP_GetEquityScripPrice] 4185,''2009-03-25 00:00:00.000''--22.9000
  
' 
END
GO
