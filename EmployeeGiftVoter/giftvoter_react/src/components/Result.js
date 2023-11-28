import React from 'react'

export default function Result(props){
    return(<>
     <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src={props.ImgUrl} alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                             <p className="fw-bolder">{`Name:${props.Name}`}</p>
                                 <h5 className="fw-bolder">{`Count:${props.Count}`}</h5>
                               </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                           
                        
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}