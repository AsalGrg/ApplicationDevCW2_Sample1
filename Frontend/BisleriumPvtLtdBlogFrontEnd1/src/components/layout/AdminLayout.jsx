import React from 'react'
import Sidebar from '../admin/Sidebar'
import { Outlet } from 'react-router'
import Navbar from '../navbar/Navbar'

const AdminLayout = () => {
  return (
    <div className='row'
    style={{
        minHeight: '100vh'
    }}
    
    >
        <div className='col-2 border border-right border-2'>
            <Sidebar/>
        </div>

        <div className='col-8 py-5 px-5'>
            <Outlet/>
        </div>
    </div>
  )
}

export default AdminLayout