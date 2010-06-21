  
  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER procedure [dbo].[SP_GetEquityScripPrice]  
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


--exec [SP_GetEquityScripPrice] 4185,'2009-03-25 00:00:00.000'--22.9000
  
 