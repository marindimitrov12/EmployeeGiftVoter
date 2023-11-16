import {useState} from 'react'
import { Link } from 'react-router-dom'
import { closeEvent } from '../services/ClientService'
import { useUserContext } from '../context/UserContext'
export default function Event(props){
    const[message,setMessage]=useState('');
    const {user}=useUserContext();
    const handleClick=async()=>{
      closeEvent(props.EventId,user.id)
      .then((res)=>{
         console.log(res);
         if(res.message===undefined){
            setMessage("Event Closed");
         }
         else  {
            setMessage(res.message);
         }
         
      })
    }
    return(<>
    <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                             <p className="fw-bolder">{`Name:${props.BirthDayBoyName}`}</p>
                                 <h5 className="fw-bolder">{`StartDate:${props.StartDate}`}</h5>
                                 <p className="fw-bolder">{`EndDate:${props.EndDate}`}</p>
                                 <p>{message}</p>
                               
                                
                                
                             </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                           
                             <div className="text-center"><button className="btn btn-outline-dark mt-auto" onClick={handleClick}>Cancel </button></div>
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}