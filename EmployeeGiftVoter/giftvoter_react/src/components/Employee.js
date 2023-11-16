import {useState} from 'react'
import { Link } from 'react-router-dom'
import {startEvent}from '../services/ClientService'
import { useUserContext } from '../context/UserContext';
export default function Employee(props){
    const[error,setError]=useState();
    const{user}=useUserContext();
    const date = new Date();
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    const currentDate = `${day}-${month}-${year}`;
     
    const handleClick=async ()=>{
       startEvent(currentDate,user.id,props.Id)
       .then((res)=>{
        console.log(res.message);
        if(res.message!==undefined){
            setError(res.message)

        }else{
            setError("Event Started!")
        }
        
       });
    }
    return(<>
     <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                             <p className="fw-bolder">{props.Id}</p>
                                 <h5 className="fw-bolder">{props.UserName}</h5>
                               
                                 {`${props.DateOfBirth}`}
                                 <p className="fw-bolder">{error}</p>
                             </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                             <div className="text-center"><button className="btn btn-outline-dark mt-auto" onClick={handleClick} >Start Event</button></div>
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}