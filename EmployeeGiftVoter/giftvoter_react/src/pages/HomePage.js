import {useState,useEffect} from 'react'
import Employee from '../components/Employee';
import {getAllEmployees}from '../services/ClientService'
export default function HomePage(){
    const[employees,setEmployees]=useState(null);

    useEffect(()=>{
        getAllEmployees()
        .then((res)=>{
            console.log(res);
            setEmployees(res);
        });
    },[]);

    return (<>
     <section className="py-5">
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
                      
                    {employees===null?<h1>Loading...</h1>:employees.map(e=><Employee 
                    Id={e.id}
                    UserName={e.username}
                    DateOfBirth={e.dateOfBirt}
                    key={e.id}/>)}
                   
                   
                </div>
            </div>
        </section>
    </>)
}