import {useState} from 'react'
import {vote} from '../services/ClientService'
import { useUserContext } from '../context/UserContext'
export default function Gift(props){
    const[error,setError]=useState('');
    const {user}=useUserContext();
    
    const handleClick=()=>{
      vote(props.EventId,props.Id,user.id)
      .then((res)=>{
        console.log(res);
        if(res.message!==undefined){
            setError(res.message);
        }
        else{
            setError("Send");
        }
        
      });
    }
    return(<>
    <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                             
                                 <h5 className="fw-bolder">{`${props.Name}`}</h5>
                               <p >{`${error}`}</p>
                               
                                
                                
                             </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                             
                            <div className="text-center"><button className="btn btn-outline-dark mt-auto"onClick={handleClick}  >Vote </button></div>
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}