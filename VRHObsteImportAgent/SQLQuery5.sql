USE [VRH_Q]
GO

/****** Object:  StoredProcedure [dbo].[ShowQueue]    Script Date: 10/31/2012 18:26:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ShowQueue]
	-- Add the parameters for the stored procedure here
	  @Counter1 as varchar,
      @Counter2 as varchar,
      @Counter3 as varchar,
      @Counter4 as varchar,
      @Counter5 as varchar
AS
BEGIN
	SELECT Queue_no,Counter_name FROM (
	SELECT  TOP(5)  ImportData.Queue_no as Queue_no,ImportData.Counter as Counter_name FROM  ImportData  WHERE   (ImportData.Other_Status IN ('4'))   AND (DATEDIFF(D, ImportData.Update_Time, GETDATE()) = 0) AND ImportData.Counter='ช่องบริการ '+ @Counter1 order by ImportData.Queue_no asc
	union all
	SELECT  TOP(5)  ImportData.Queue_no as Queue_no,ImportData.Counter as Counter_name FROM  ImportData  WHERE   (ImportData.Other_Status IN ('4'))   AND (DATEDIFF(D, ImportData.Update_Time, GETDATE()) = 0) AND ImportData.Counter='ช่องบริการ '+ @Counter2 order by ImportData.Queue_no asc
	union all
	SELECT  TOP(5)  ImportData.Queue_no as Queue_no,ImportData.Counter as Counter_name FROM  ImportData  WHERE   (ImportData.Other_Status IN ('4'))   AND (DATEDIFF(D, ImportData.Update_Time, GETDATE()) = 0) AND ImportData.Counter='ช่องบริการ '+ @Counter3 order by ImportData.Queue_no asc
	union all
	SELECT  TOP(5)  ImportData.Queue_no as Queue_no,ImportData.Counter as Counter_name FROM  ImportData  WHERE   (ImportData.Other_Status IN ('4'))   AND (DATEDIFF(D, ImportData.Update_Time, GETDATE()) = 0) AND ImportData.Counter='ช่องบริการ '+ @Counter4 order by ImportData.Queue_no asc
	union all
	SELECT  TOP(5)  ImportData.Queue_no as Queue_no,ImportData.Counter as Counter_name FROM  ImportData  WHERE   (ImportData.Other_Status IN ('4'))   AND (DATEDIFF(D, ImportData.Update_Time, GETDATE()) = 0) AND ImportData.Counter='ช่องบริการ '+ @Counter5 order by ImportData.Queue_no asc
	) TB 
	Order by Counter_name ASC
END

GO


