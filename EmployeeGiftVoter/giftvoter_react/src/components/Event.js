import React from 'react'
import { Link } from 'react-router-dom'
export default function Event(props){
    return(<>
    <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                             <p className="fw-bolder">{`Name:${props.BirthDayBoyName}`}</p>
                                 <h5 className="fw-bolder">{`StartDate:${props.StartDate}`}</h5>
                                 <p className="fw-bolder">{`EndDate:${props.EndDate}`}</p>
                               
                                
                                
                             </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                             {props.EndDate!==''&&<div className="text-center"><Link className="btn btn-outline-dark mt-auto" to={`/eventResultsPage/${props.Id}`} >Check Results </Link></div>}
                             {props.EndDate===''&&<div className="text-center"><Link className="btn btn-outline-dark mt-auto"to={`/eventVotePage/${props.Id}`} >Vote </Link></div>}
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}