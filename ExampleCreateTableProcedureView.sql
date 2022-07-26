-- Students Table
-- id  | subject | marks 
-- ------+---------+-------
-- 1001 | English |    88
-- 1002 | English |    90
-- 1001 | Maths   |    70
-- 1002 | Maths   |    65
-- 1001 | Science |    70
-- 1002 | Science |    65
-- 1001 | Social  |    70
-- 1002 | Social  |    65

---------Task to transform the students table into below transposed format---------

-- id  | English | Maths | Science | Social 
-- ------+---------+-------+---------+--------
-- 1001 | 88      | 70    | 70      | 70
-- 1002 | 90      | 65    | 65      | 65

-- create a table
CREATE LANGUAGE plpgsql;
CREATE TABLE students (
  id INTEGER,
  subject TEXT NOT NULL,
  marks INTEGER NOT NULL
);
-- insert some values
INSERT INTO students VALUES (1001, 'English', 88);
INSERT INTO students VALUES (1002, 'English', 90);
INSERT INTO students VALUES (1001, 'Maths', 70);
INSERT INTO students VALUES (1002, 'Maths', 65);
INSERT INTO students VALUES (1001, 'Science', 70);
INSERT INTO students VALUES (1002, 'Science', 65);
INSERT INTO students VALUES (1001, 'Social', 70);
INSERT INTO students VALUES (1002, 'Social', 65);

select * from students;

--Solution 1: Using max and case statement - Drawback is for each new column - manually add the line
select id,
max(case when (subject='English') then marks else NULL end) as English,
max(case when (subject='Maths') then marks else NULL end) as Maths,
max(case when (subject='Science') then marks else NULL end) as Science,
max(case when (subject='Social') then marks else NULL end) as Social
FROM students
group by id
order by id;

--Solution 2: Using procedures - create dynamic json and create columns from that
create procedure transposingRowsToColumns() 
as $$
declare
  l_sql text;
  l_columns text;
begin
  select string_agg(distinct format('(props ->> %L) as %I', subject, subject), ', ')
    into l_columns
  from students;
  
  l_sql := 
    'create view transposedTable as 
     select id, '||l_columns||' 
     from (
      select id, jsonb_object_agg(subject, marks) as props
       from students 
       group by id 
       order by id
     ) t';
  execute l_sql;
end;
$$
language plpgsql;

call transposingRowsToColumns();  

select *
from transposedTable;